
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 8:26:20
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

Namespace KTB.DNET.BusinessFacade
    Public Class BabitIklanDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitIklanDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BabitIklanDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitIklanDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitIklanDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitIklanDetail
            Return CType(m_BabitIklanDetailMapper.Retrieve(ID), BabitIklanDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitIklanDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitIklanDetailCode", MatchType.Exact, Code))

            Dim BabitIklanDetailColl As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias)
            If (BabitIklanDetailColl.Count > 0) Then
                Return CType(BabitIklanDetailColl(0), BabitIklanDetail)
            End If
            Return New BabitIklanDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitIklanDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitIklanDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitIklanDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitIklanDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitIklanDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitIklanDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitIklanDetail As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias)
            Return _BabitIklanDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitIklanDetailColl As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitIklanDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitIklanDetail), SortColumn, sortDirection))
            Dim BabitIklanDetailColl As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitIklanDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitIklanDetailColl As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitIklanDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitIklanDetailColl As ArrayList = m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitIklanDetail), columnName, matchOperator, columnValue))
            Return BabitIklanDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitIklanDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), columnName, matchOperator, columnValue))

            Return m_BabitIklanDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "BabitIklanDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitIklanDetail), "BabitIklanDetailCode", AggregateType.Count)
            Return CType(m_BabitIklanDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitIklanDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitIklanDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitIklanDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitIklanDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitIklanDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitIklanDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitIklanDetail)
            Try
                m_BabitIklanDetailMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is BabitHeader) Then
                CType(InsertArg.DomainObject, BabitHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitIklanDetail) Then
                CType(InsertArg.DomainObject, BabitIklanDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objBabitHeader As BabitHeader, ByVal arrIklanDetail As ArrayList, ByVal arrDocument As ArrayList, ByVal arrDealerAlloc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objBabitHeader, m_userPrincipal.Identity.Name)

                    If Not IsNothing(arrDealerAlloc) Then
                        If arrDealerAlloc.Count > 0 Then
                            For Each oDealerAlloc As BabitDealerAllocation In arrDealerAlloc
                                oDealerAlloc.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oDealerAlloc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDocument) Then
                        If arrDocument.Count > 0 Then
                            For Each oDocument As BabitDocument In arrDocument
                                oDocument.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oDocument, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrIklanDetail) Then
                        If arrIklanDetail.Count > 0 Then
                            For Each oIklanDetail As BabitIklanDetail In arrIklanDetail
                                oIklanDetail.BabitHeader = objBabitHeader
                                m_TransactionManager.AddInsert(oIklanDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBabitHeader.ID
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


        Function DeleteTransaction(ByVal objBabitHeader As BabitHeader, ByVal arrIklanDetail As ArrayList, ByVal arrDocument As ArrayList, Optional ByVal objBabitDealerAllocation As BabitDealerAllocation = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIklanDetail.Count > 0 Then
                        For Each oPameranDetail As BabitIklanDetail In arrIklanDetail
                            oPameranDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oPameranDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDocument.Count > 0 Then
                        For Each oDocument As BabitDocument In arrDocument
                            oDocument.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(oDocument, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If Not IsNothing(objBabitDealerAllocation) Then
                        If objBabitDealerAllocation.ID > 0 Then
                            objBabitDealerAllocation.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(objBabitDealerAllocation, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    objBabitHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_TransactionManager.AddUpdate(objBabitHeader, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
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

        Public Function UpdateTransaction(babitHeader As BabitHeader, arrBabitMediaIklan As ArrayList, arrDelBabitMediaIklan As ArrayList, arrBabitDocs As ArrayList, arrDelBabitDocs As ArrayList, _arrBabitDealerAlloc As ArrayList, _arrDelBabitDealerAlloc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrDelBabitMediaIklan) Then
                        If arrDelBabitMediaIklan.Count > 0 Then
                            For Each item As BabitIklanDetail In arrDelBabitMediaIklan
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitMediaIklan) Then
                        If arrBabitMediaIklan.Count > 0 Then
                            For Each item As BabitIklanDetail In arrBabitMediaIklan
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arrDelBabitDocs) Then
                        If arrDelBabitDocs.Count > 0 Then
                            For Each item As BabitDocument In arrDelBabitDocs
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBabitDocs) Then
                        If arrBabitDocs.Count > 0 Then
                            For Each item As BabitDocument In arrBabitDocs
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(_arrDelBabitDealerAlloc) Then
                        If _arrDelBabitDealerAlloc.Count > 0 Then
                            For Each item As BabitDealerAllocation In _arrDelBabitDealerAlloc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(_arrBabitDealerAlloc) Then
                        If _arrBabitDealerAlloc.Count > 0 Then
                            For Each item As BabitDealerAllocation In _arrBabitDealerAlloc
                                item.BabitHeader = babitHeader
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    m_TransactionManager.AddUpdate(babitHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = babitHeader.ID
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

